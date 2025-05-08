import { LockOutlined } from "@mui/icons-material";
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { Box, Button, Container,  Paper, TextField, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { loginSchema, LoginSchema } from "../../lib/schemas/loginschema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useLazyUserInfoQuery, useLoginMutation } from "./accountApi";


export default function LoginForm() {
    const [login, {isLoading}] = useLoginMutation();
    const [fetchUserInfo] = useLazyUserInfoQuery();
    const location = useLocation();
    const {register, handleSubmit, formState: {errors}} = useForm<LoginSchema>({
        mode: 'onTouched',
        resolver: zodResolver(loginSchema)
    });
   
    const navigate = useNavigate();
    const onSubmit = async (data: LoginSchema) => {
        try {
            await login(data);// ✅ unwrap to catch any errors
           await fetchUserInfo().unwrap(); // Fetch user info after login
            navigate(location.state?.from || '/catalog');
        } catch (err) {
            console.error("Login failed:", err);
        }
    }
    
    return (
        <Container component={Paper} maxWidth='sm' sx={{ borderRadius: 3 }}>
        <Box display='flex' flexDirection='column' alignItems='center' mt={8}>
          <LockOutlined sx={{ mt: 3, color: 'secondary.main', fontSize: 40 }} />
          <Typography variant="h5" >sign in</Typography>
        </Box>
      
        <Box
          component='form'
          onSubmit={handleSubmit(onSubmit)}
          width='100%'
          display='flex'
          flexDirection='column'
          gap={3}
          my={3}
        >
          <TextField
            fullWidth
            label='Email'
            autoFocus
            {...register('email')}
            error={!!errors.email}
            helperText={errors.email ?.message}
          />
          <TextField
            fullWidth
            label='Password'
            type='password'
            {...register('password')}
            error={!!errors.password}
            helperText={errors.password ?.message}

          />
         <Button disabled={isLoading} variant='contained' type="submit">sign in</Button>

          <Typography sx={{ textAlign: 'center' }}>
            Don't have an account?
            <Typography
              component={Link}
              to='/register'
              color='primary.main'
              fontWeight='bold'
              sx={{ ml: 2, display: 'inline' }}
            >
              sign up
            </Typography>
          </Typography>
        </Box>
      </Container>
      
    )
}