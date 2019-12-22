import React, {useState} from 'react';
import {NavLink} from "react-router-dom";
import {
    Typography,
    Avatar,
    Button,
    CssBaseline,
    Grid,
    Checkbox,
    FormControlLabel,
    TextField,
    Container, Fab
} from '@material-ui/core';
import { withStyles  } from '@material-ui/core/styles';
import AccountBoxIcon from "@material-ui/icons/AccountBox";
import {useUser} from "../services/UseUser";
import {API_BASE_URL} from "../config";

const styles = theme => ({
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
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
});
 function Login  (props) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const {setAccessToken} = useUser();
    const { classes } = props;

    function handleEmailChange(event) {
        setEmail(event.target.value);
    };

    function handlePasswordChange(event) {
        setPassword(event.target.value);
    };

    function handleFormSubmit(event) {
        event.preventDefault();
        getToken(email, password);
    };

    const getToken = async (email, password) => {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
                }
        };
        const url = `${API_BASE_URL}/Account/Login?email=${email}&password=${password}`;
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
                    <AccountBoxIcon/>
                </Avatar>
                <Typography component="h1" variant="h5">
                   Авторизация
                </Typography>
                <form className={classes.form} noValidate>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email адрес"
                        name="email"
                        autoComplete="email"
                        autoFocus
                        onChange={handleEmailChange}
                        value={email}
                    />
                    <TextField
                        variant="outlined"
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Пароль"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                        onChange={handlePasswordChange}
                        value={password}
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={handleFormSubmit}
                        className={classes.submit}
                    >
                        Авторизоваться
                    </Button>
                    <Grid container>
                        <Grid item>
                            <NavLink to='/register' variant="body2">
                                {"Нет аккаунта? Зарегистрироваться"}
                            </NavLink>
                        </Grid>
                    </Grid>
                </form>
            </div>

        </Container>
    );
}
export default withStyles(styles)(Login);
