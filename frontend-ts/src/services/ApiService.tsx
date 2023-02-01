import React from "react";
import authStateProvider from "./AuthStateProvider";
import ApiResult from "../Models/ApiResult";

export default function MakeApiCall<TInput, TResult>(
    path: string = "/", method: string = "GET", data: TInput )
    : Promise<ApiResult<TResult>>{
    const requestUrl = `https://localhost:7282/${path}`;
    let requestInit: RequestInit = {};

    requestInit.method = method;
    if (authStateProvider.isLoggedIn){
            requestInit.headers = {
            'Content-Type': 'application/json' ,
            'Authorization': 'Bearer ' + authStateProvider.authToken
        }
    }
    else{
        requestInit.headers = {
            'Content-Type': 'application/json'
        }
    }
    if(data !== null){
        requestInit.body = JSON.stringify(data);
    }

    return fetch(requestUrl, requestInit)
        .then(res => res.json())
        .then(res => res as ApiResult<TResult>)
}