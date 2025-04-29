import { debounce, TextField } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/store";
import { setSearchTerm } from "./catalogSlice";
import { useInsertionEffect, useState } from "react";


export default function Search() {
    const {searchTerm} = useAppSelector(state => state.catalog);
    const dispatch = useAppDispatch();
    const [term, setTerm] = useState(searchTerm);

    useInsertionEffect(() =>{
        setTerm(searchTerm)
    }, [searchTerm]);

    const debounceSearch = debounce(event =>{
        dispatch(setSearchTerm(event.target.value))
    }, 500)
    return (
        <TextField
            label='Search Products'
            variant="outlined"
            fullWidth
            type="search"
            value= {term}
            onChange={e => {
                setTerm(e.target.value);
                debounceSearch(e);
            }}
            ></TextField>

    )
}