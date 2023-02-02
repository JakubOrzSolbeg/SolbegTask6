import React from "react";
import MakeApiCall from "../../services/ApiService";
import SettingsList from "../../Models/Settings";
import LoginRequest from "../../Models/LoginRequest";
import authStateProvider from "../../services/AuthStateProvider";

type SettingsFormState = {
    newDailyLimit: string
    errors: string
}

export class SettingsForm extends React.Component<any, SettingsFormState>{

    constructor(props: any) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
        this.handle_submit = this.handle_submit.bind(this);

    }

    state: SettingsFormState = {
        newDailyLimit: "",
        errors: ""
    }

    componentDidMount() {
        MakeApiCall<null, SettingsList>("Accounts/Settings", "GET", null)
            .then(res => {
                if(res.isSuccess){

                    this.setState({
                        newDailyLimit: (((res.body?.maxDailySpending ?? 0)) / 100.0 ).toString() ?? ""}
                    )
                }
            })
    }

    handle_submit(event: any){
        event.preventDefault();

        let requestData = {
            newDailyLimit: Math.trunc(parseFloat(this.state.newDailyLimit) * 100)
        }
        if (requestData.newDailyLimit > 0){
            MakeApiCall<any, boolean>("Accounts/EditAccount", "PUT", requestData)
                .then(res => {
                    if (!res.isSuccess){
                        this.setState({errors: res.errors})
                    }
                    console.log(res);
                })
        }

    }


    handleChange(evt: React.ChangeEvent<HTMLInputElement>){
        const value = evt.target.value;
        this.setState({
            ...(this.state),
            [evt.target.name]: value
        });
    }

    render() {
        return (
            <div className={"settings-panel"}>
                <h2>Account settings</h2>
                <form className={"settings-form"} onSubmit={this.handle_submit}>

                    <label>Max daily spending limit:
                        <input id={"newDailyLimit"} name={"newDailyLimit"} type={"number"} required={true}
                            value={this.state.newDailyLimit} onChange={this.handleChange}/>
                    </label>
                    <span id={"form-errors"}>{this.state.errors}</span>
                    <input className={"save-button"} type={"submit"} value={"Save changes"} />
                </form>
            </div>
        )
    }

}
