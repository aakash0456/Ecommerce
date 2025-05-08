import { z } from "zod";


const passwordRegex = new RegExp(
    /(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$/
);
export const registerSchema = z.object({
    email: z.string().email("Invalid email address"),
    password: z.string().regex(passwordRegex, {
        message:
            "Password must be 6-10 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.",

    })
}
);


export type RegisterSchema = z.infer<typeof registerSchema>;