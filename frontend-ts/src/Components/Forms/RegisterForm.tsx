import React from "react";
import MakeApiCall from "../../services/ApiService";
import LoginRequest from "../../Models/LoginRequest";
import authStateProvider from "../../services/AuthStateProvider";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faLeftLong} from "@fortawesome/free-solid-svg-icons";

type RegisterFormState = {
    login: string,
    password: string,
    passwordRepeat: string,
    errors: string,
}

export default class RegisterForm extends React.Component<any, RegisterFormState>{
    constructor(props: any) {
        super(props);

        this.handle_submit = this.handle_submit.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.validate =this.validate.bind(this);
    }
    state: RegisterFormState = {
        login: "",
        password: "",
        passwordRepeat: "",
        errors: ""
    }

    validate(): boolean{
        if (this.state.password !== this.state.passwordRepeat){
            this.setState({errors: "Passwords doesn't match"})
            return false;
        }

        return true;
    }

    handle_submit(event: any){
        if(this.validate()){

            let requestData: LoginRequest = {
                login: this.state.login,
                password: this.state.password
            }

            MakeApiCall<LoginRequest, string>("Accounts/Register", "POST", requestData)
                .then(result => {
                    if (result.isSuccess){
                        authStateProvider.login(result.body!)
                    }
                    else{
                        this.setState({errors: result.errors})
                    }
                })
        }
        event.preventDefault();
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
            <div className={"main_panel"}>
                <button onClick={() => window.location.replace("/")}
                        id={"return-button"} className={"transparent-button"} >
                    <FontAwesomeIcon className={"return-button"} icon={faLeftLong} />
                </button>
                <form className={"login-form"} onSubmit={this.handle_submit}>
                    <h3>Register</h3>
                    <label htmlFor={"login"}>Username: </label>
                    <input id={"login"} name={"login"} type={"text"} required={true} minLength={6}
                           value={this.state.login} onChange={this.handleChange}/>
                    <label htmlFor={"password"}>Password: </label>
                    <input id={"password"} name={"password"} type={"password"} minLength={6}
                           required={true} value={this.state.password} onChange={this.handleChange}/>
                    <label htmlFor={"passwordRepeat"}>Repeat password: </label>
                    <input id={"passwordRepeat"} name={"passwordRepeat"} type={"password"} minLength={6}
                           required={true} value={this.state.passwordRepeat} onChange={this.handleChange}/>
                    <span id={"form-errors"}>{this.state.errors}</span>
                    <input className={"primary-form-button"} type={"submit"} value={"Register"} />
                </form>
            </div>
        )
    }

}