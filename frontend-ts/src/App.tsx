import React from 'react';
import { observer } from "mobx-react"

import authStateProvider from "./services/AuthStateProvider";
import NotLoggedApp from "./Roots/NotLoggedApp";
import {LoggedMainPage} from "./Roots/LoggedMainPage";

const App = observer(() => {

        console.log(authStateProvider.isLoggedIn);
        if (!authStateProvider.isLoggedIn) {
            return (
                <NotLoggedApp />
            );
        }
        else{
            return (
                <LoggedMainPage />
            )
        }
}
);

export default App;
