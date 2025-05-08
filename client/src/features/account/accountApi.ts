import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQueryWithErrorHandling } from "../../app/api/baseApi";
import { User } from "../../app/models/user";
import { LoginSchema } from "../../lib/schemas/loginschema";
import { router } from "../../app/routes/Routes";
import { toast } from "react-toastify";

export const accountApi = createApi({
    reducerPath: "accountApi",
    baseQuery: baseQueryWithErrorHandling,
    tagTypes: ["UserInfo"],
    endpoints: (builder) => ({
        login: builder.mutation<void, LoginSchema>({
            query: (creds) => {
                return {
                url: "/login?useCookies=true",
                method: "POST",
                body: creds,
            }},

            async onQueryStarted(_, {dispatch, queryFulfilled })  {
                try {
                    await queryFulfilled;
                    dispatch(
                        accountApi.util.invalidateTags(["UserInfo"])
                    );
                    
                  } catch (err) {
                    console.error(err);
                  }
            }
        }),
        register: builder.mutation<void, object>({
            query: (creds) => ({
                url: "account/register",
                method: "POST",
                body: creds,
            }),

            async onQueryStarted(_, {queryFulfilled })  {
                try {
                    await queryFulfilled;
                    toast.success("Registration successful! Please log in.");
                    router.navigate("/login");
                    
                  } catch (err) {
                    console.error(err);
                    throw err;
                  }
            }
        }),
        userInfo: builder.query<User, void>({
            query: () => "account/user-info",
            providesTags: ["UserInfo"]
            
        }), 
        logout: builder.mutation<void, void>({
            query: () => ({
                url: "account/logout",
                method: "POST",
            }),

            async onQueryStarted(_, {dispatch, queryFulfilled })  {
                
                    await queryFulfilled;
                    dispatch(
                        accountApi.util.invalidateTags(["UserInfo"])
                    );
                    router.navigate("/");
                    
                 
            }
        }),
    }),
});



export const {
    useLoginMutation,
    useRegisterMutation,
    useLogoutMutation,
    useUserInfoQuery, // ✅ include this too if you're using the query
    useLazyUserInfoQuery
  } = accountApi;
  