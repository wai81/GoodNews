import { authentication_service} from "../services/authentication_service";

export function authHeader() {
    // return authorization header with jwt token
    const currentUser = authentication_service.currentUserValue;
    if (currentUser && currentUser.token) {
        return { Authorization: `Bearer ${currentUser.token}` };
    } else {
        return {};
    }
}