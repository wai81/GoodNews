//this guide https://codious.io/user-management-with-react-hooks/
import React, {
    createContext,
    useContext,
    useEffect,
    useState} from 'react';

const getCurrentUser = (accessToken) => {
    return {
        email: accessToken,
    };
}

const initialState = {
    user: {},
    accessToken: undefined,
};

const UserContext = createContext(initialState);

export function UserProvider({children}) {
    const [accessToken, setAccessToken] = useState(localStorage.getItem('access_token'));
    const [user, setUser] = useState({});

    function handleAccessTokenChange() {
        const savedAccessToken = localStorage.getItem('access_token');
        if (!savedAccessToken || savedAccessToken === 'null') {
            localStorage.setItem('access_token', accessToken);
        }

        if (!user.email && accessToken) {
            const user = getCurrentUser(accessToken);
            setUser(user);
        } else if (!accessToken) {
            // Log out
            localStorage.removeItem('access_token');
            setUser({});
        }
    }

    useEffect(() => {
        handleAccessTokenChange();
    }, [accessToken]);

    return (
        <UserContext.Provider value={{user, accessToken, setAccessToken}}>
            <>
                {children}
            </>
        </UserContext.Provider>
    );
}
export const useUser = () => useContext(UserContext);
