import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material"
import { Product } from "../../app/models/product"

type Props ={

    product : Product
}

export default function ProductCard({product} : Props) {
  return (
  <Card  elevation={3}
  sx={{
    width : 280,
    borderRadius:2,
    display: 'flex',
    flexDirection: 'column',
    justifyContent:'space-between'

  }}>

 <CardMedia sx={{height : 240, backgroundSize : 'cover'}}
  image={product.pictureUrl}
   title = {product.name}>
     </CardMedia>
    <CardContent> 
        <Typography gutterBottom 
        sx = {{color : 'uppercase'}}
        variant = "subtitle2"> {product.name}</Typography>
         <Typography 
        sx = {{color : 'secondary.main'}}
        variant = "h6">
        ${(product.price / 100).toFixed(2)} </Typography>
    </CardContent>
    <CardActions
     sx = {{justifyContent: 'space-button'}}
       >
        <Button>Add to Cart</Button>
        <Button>View</Button>
       </CardActions>
  
  </Card>

  )
}