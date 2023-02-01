import React from "react";
import authStateProvider from "../../services/AuthStateProvider";
import LoginRequest from "../../Models/LoginRequest";
import MakeApiCall from "../../services/ApiService";

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
            .then(res => console.log(res));
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
                <form className={"login-form"} onSubmit={this.handle_submit}>
                    <h3>Log in to continue</h3>
                    <label htmlFor={"login"}>Login: </label>
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