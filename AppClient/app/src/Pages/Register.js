import React,{useState} from 'react';
import {NavLink} from "react-router-dom";
import {
    Typography,
    Avatar,
    Button,
    CssBaseline,
    Grid,
    TextField,
    Container} from '@material-ui/core';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import { makeStyles } from '@material-ui/core/styles';
import {API_BASE_URL} from "../config";
import {useUser} from "../services/UseUser";



const useStyles = makeStyles(theme => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(3),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

const Register = () => {
    const classes = useStyles();
    const {setAccessToken} = useUser();
    const [emailInput, setEmailInput] = useState('');
    const [passwordInput, setPasswordInput] = useState('');
    const [confirmPasswordInput, setConfirmPasswordInput] = useState('');

       function handleEmailChange(event) {
           setEmailInput(event.target.value);
        };

        function handlePasswordChange(event) {
            setPasswordInput(event.target.value);
        };

        function handleConfirmPasswordChange(event) {
            setConfirmPasswordInput(event.target.value);
        };
        function submitHandler(event) {
            event.preventDefault();
            if(passwordInput === confirmPasswordInput) {
                getToken(emailInput, passwordInput, confirmPasswordInput);
            }
        };

        const getToken = async (emailInput, passwordInput) => {
             const requestOptions = {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        };
             const url = `${API_BASE_URL}/Account/Register?email=${emailInput}&password=${passwordInput}`
            fetch(url,requestOptions)
                .then((response) => {if (!response.ok) throw new Error(response.status);
                else return response.text();})
                //.then(response => response.text())
                .then((text) => {
                    const email = JSON.parse(text).email;
                    setAccessToken(email);
                    const token = JSON.parse(text).token;
                    const role = JSON.parse(text).role
                })
                .catch( err =>
                {alert(err)});
        };



    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Регистрация
                </Typography>
                <form className={classes.form} noValidate>
                    <Grid container spacing={2}>

                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                required
                                fullWidth
                                id="email"
                                label="Email Address"
                                name="email"
                                autoComplete="email"
                                onChange={handleEmailChange}
                                value={emailInput}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="current-password"
                                onChange={handlePasswordChange}
                                value={passwordInput}
                            />
                        </Grid>
                        <Grid item xs={12}>
                        <TextField
                            variant="outlined"
                            required
                            fullWidth
                            name= "Confirm password"
                            label="Confirm password"
                            type="password"
                            id="confirmPassword"
                            autoComplete="current-password"
                            onChange={handleConfirmPasswordChange}
                            value={confirmPasswordInput}
                        />
                        </Grid>

                    </Grid>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={submitHandler}
                        className={classes.submit}
                    >
                        Регистрация
                    </Button>
                    <Grid container justify="flex-end">
                        <Grid item>
                            <NavLink to="/Login" variant="body2">
                                Уже есть аккаунт? Войти в систему
                            </NavLink>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}
export default Register;
