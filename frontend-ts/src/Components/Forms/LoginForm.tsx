import React from "react";
import authStateProvider from "../../services/AuthStateProvider";
import LoginRequest from "../../Models/LoginRequest";
import MakeApiCall from "../../services/ApiService";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faLeftLong} from "@fortawesome/free-solid-svg-icons";

type LoginFormState = {
    login: string,
    password: string,
    errors: string,
}

export default class LoginForm extends React.Component<any, LoginFormState>{
    constructor(props: any) {
        super(props);

        this.handle_submit = this.handle_submit.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }
    state: LoginFormState = {
        login: "",
        password: "",
        errors: "",
    }

    handle_submit(event: any){
        event.preventDefault();
        let requestData: LoginRequest = {
            login: this.state.login,
            password: this.state.password
        }
        console.log(requestData);

        MakeApiCall<LoginRequest, string>("Accounts/Login", "POST", requestData)
            .then(res => {
                if (res.isSuccess){
                    authStateProvider.login(res.body!)
                }
                else{
                    this.setState({errors: res.errors})
                }
            });
    }


    handleChange(evt: React.ChangeEvent<HTMLInputElement>){
        const value = evt.target.value;
        this.setState({
            ...(this.state),
            [evt.target.name]: value
        });
    }

    render(){
        return(
            <div className={"main_panel"} >
                <button onClick={() => window.location.replace("/")}
                        id={"return-button"} className={"transparent-button"} >
                    <FontAwesomeIcon className={"return-button"} icon={faLeftLong} />
                </button>
                <form className={"login-form"} onSubmit={this.handle_submit}>
                    <h3>Log in to continue</h3>
                    <label htmlFor={"login"}>Username: </label>
                    <input id={"login"} name={"login"} type={"text"} required={true}
                           value={this.state.login} onChange={this.handleChange}/>
                    <label> Password: </label>
                    <input id={"password"} name={"password"} type={"password"}
                           required={true} value={this.state.password} onChange={this.handleChange}/>
                    <span id={"form-errors"}>{this.state.errors}</span>
                    <input className={"primary-form-button"} type={"submit"} value={"Log in"} />
                </form>
            </div>
        )
    }

}