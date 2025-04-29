import { createSlice } from "@reduxjs/toolkit/react";
import { ProductParams } from "../../app/models/productParams";

const initialState: ProductParams = {
    pageNumber : 1,
    pageSize : 8,
    types: [],
    brands: [],
    searchTerm: '',
    orderby: 'name'
}
export const catalogSlice = createSlice({
    name : 'catalogSlice',
    initialState,
    reducers:{
        setPageNumber(state, action){
            state.pageNumber = action.payload
        },
        setpageSize(state, action){
            state.pageSize = action.payload
        },
        setOrderBy(state, action){
            state.orderby = action.payload
            state.pageNumber =1;
        },
        setTypes(state, action){
            state.types = action.payload
            state.pageNumber =1;
        },
        setBrands(state, action){
            state.brands = action.payload
            state.pageNumber =1;
        },
        setSearchTerm(state, action){
            state.searchTerm = action.payload
            state.pageNumber =1;
        },

        resetParams(){
            return initialState;
        }
    }
});


export const{setBrands, setOrderBy, setPageNumber, setSearchTerm, setpageSize, setTypes, resetParams} = catalogSlice.actions;