import React from "react";
import {useNavigate} from "react-router-dom";
import authStateProvider from "../../services/AuthStateProvider";

export default function Navigation(){
    const navigate = useNavigate();

    const logout = () =>{
        authStateProvider.logout();
    }

    return(
        <div className={"NavBar"}>
            <div className={"logo"}>
                <h1 onClick={() => navigate("/") }>
                    Jakubo Bank
                </h1>
            </div>
            <button onClick={logout}> Logout </button>
        </div>
    )
}