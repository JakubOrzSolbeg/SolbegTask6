import React from "react";
import {useNavigate} from "react-router-dom";
import authStateProvider from "../../services/AuthStateProvider";
import { faGear } from "@fortawesome/free-solid-svg-icons";
import { faArrowRightFromBracket } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

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
            <button className={"nav-button"} onClick={() => navigate("/settings")}> <FontAwesomeIcon icon={faGear} /> </button>
            <button className={"nav-button"} onClick={logout}> <FontAwesomeIcon icon={faArrowRightFromBracket} /> </button>
        </div>
    )
}