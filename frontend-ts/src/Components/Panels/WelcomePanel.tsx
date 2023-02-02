import React from "react";
import { useNavigate } from "react-router-dom";

export default function WelcomePanel(){
    const navigate = useNavigate();

    const move_to_login_page = () =>{
        navigate("/login")
    }

    const  move_to_register_page = () => {
        navigate("/register")
    }


    return(
        <div className={"main_panel animate_panel"}>
            <div> Welcome in your personal banking app. We will help you keep track of all your subscriptions. </div>
            <div className={"login-action-buttons"}>
                <button id={"login_button"} onClick={move_to_login_page}> Log in </button>
                <button id={"register_button"} onClick={move_to_register_page}> Register </button>
            </div>
        </div>
    );
}