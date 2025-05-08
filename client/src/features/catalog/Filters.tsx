import { Box,  Button,  Paper } from "@mui/material";

import Search from "./Search";
import RadioButtonGroup from "../../app/shared/components/RadioButtonGroup";
import { useAppDispatch, useAppSelector } from "../../app/store/store";
import { resetParams, setBrands, setOrderBy, setTypes } from "./catalogSlice";
import CheckboxButton from "../../app/shared/components/CheckboxButton";


const sortOptions = [
    {value : 'name', label : 'Alphabetical'},
    {value : 'priceDesc', label : 'Price: High to low'},
    {value : 'price', label : 'Price: low to high'}
]

type Props ={
    filtersData:{
        brands: string[];
        types: string[];
    }
}

export default function Filters({filtersData: data}: Props) {

    
    const {orderby, types, brands} = useAppSelector(state => state.catalog)
    const dispatch = useAppDispatch();

    
 
  return (
    <Box display='flex' flexDirection= 'column' gap={3}>
        <Paper>
            <Search></Search>
        </Paper>
        <Paper
        sx = {{p : 3}}>
        <RadioButtonGroup selectedValue={orderby}
                         options = {sortOptions}
                         onChange={e => dispatch(setOrderBy(e.target.value))}
                          ></RadioButtonGroup>
        </Paper>
        <Paper sx ={{p:3}}>
            <CheckboxButton 
                 items={data.brands}
                 checked={brands}
                 onChange={(items: string[])=> dispatch(setBrands(items))}
              ></CheckboxButton>
        </Paper>

        <Paper sx ={{p:3}}>
        <CheckboxButton 
                 items={data.types}
                 checked={types}
                 onChange={(items: string[])=> dispatch(setTypes(items))}
              ></CheckboxButton>
        </Paper>
        <Button onClick={() => dispatch(resetParams())}>RESET FILTERS</Button>
    </Box>
  )
}