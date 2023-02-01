import React from "react";
import authStateProvider from "../../services/AuthStateProvider";

export default function LoginForm (){
    const login = () => {
        authStateProvider.login("yoooo");
    }

    return(
        <div className={"main_panel"}>
            <p> Yoooo login form </p>
            <button onClick={login}> Sign in </button>
        </div>
    )
}