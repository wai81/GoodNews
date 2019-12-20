import { BehaviorSubject } from 'rxjs';
import { handle_response} from "../helpers/handle_response";
import {API_BASE_URL} from "../config";

const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authentication_service = {
    login,
    logout,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function login(email, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' }
    };

    return fetch(`${API_BASE_URL}/login?email=${email}&password=${password}`, requestOptions)
        .then(handle_response)
        .then(
            (user) => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                if (user !== null && user !== '') {
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    currentUserSubject.next(user);

                    return user;
                } else {
                    return null;
                }

            }
        );
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
}