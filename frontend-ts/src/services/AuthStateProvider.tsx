import { makeAutoObservable } from "mobx"

let instance: AuthStateProvider;

export class AuthStateProvider{
    isLoggedIn: boolean = false
    authToken: string | null = ""

    constructor() {
        if (instance) {
            throw new Error("You can only create one instance!");
        }
        instance = this;
        makeAutoObservable(this);
        this.authToken = localStorage.getItem("authtoken");
        this.isLoggedIn = this.authToken !== null;
    }

    getInstance(){
        return this;
    }

    login(token: string){
        this.authToken = token;
        localStorage.setItem("authtoken", token);
        this.isLoggedIn = true;
        window.history.replaceState(null, "", "/");
    }

    logout(){
        this.isLoggedIn = false;
        this.authToken = null;
        localStorage.removeItem("authtoken");
    }
}

const authStateProvider = Object.freeze(new AuthStateProvider());
export default authStateProvider;