import React from "react";


export default function TestPage (){
    const logout = () => {
        console.log("Logout");
    }

    return(
        <div>
            <p> You are now logged in</p>
            <button onClick={logout}> Logout </button>
        </div>
    )


}
