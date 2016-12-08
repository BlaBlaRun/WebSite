import { Http, Response, Headers, RequestOptions, Request, RequestMethod} from '@angular/http';

import {Observable} from 'rxjs/Observable';
import 'rxjs/Rx';

export class HttpHelpers {
    constructor(private _http: Http) {
    }

    get haserror(): boolean {
        return this.errormsg != null;
    }

    errormsg: string;

    getaction<T>(path: string) {
        return this._http.get(path)
            .map(res => {
                let result = <T>res.json();
                return result;
            })
            .catch(this._handleError);
    }

    //postaction<T>(param: T, path: string) {
    //    this.errormsg = null;

    //    let body = JSON.stringify(param);
    //    let headers = new Headers({ 'Content-Type': 'application/json' });
    //    let options = new RequestOptions({ headers: headers });

    //    return this._http.post(path, body, options)
    //        .map(m => {
    //            var jsonresult = <Models.ViewModel.JSONReturnVM<T>>m.json();

    //            if (jsonresult.haserror) {
    //                this.errormsg = jsonresult.errormessage;
    //            }

    //            return jsonresult;
    //        })
    //        .catch(this._handleError);
    //}
    
    protected postRequest(url: string, data: any) {
        let _body = JSON.stringify(data);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions = new RequestOptions({
            method: RequestMethod.Post,
            url: url,
            headers: headers,
            body: JSON.stringify(data)
        })

        return this._http.request(new Request(requestoptions))
            .map((res: Response) => {
                if (res) {
                    return [{ status: res.status, json: res.json() }]
                }
            })
            .catch(this._handleError);
    }

    private _handleError(error: Response) {
        return Observable.throw(error.text() || 'Server error');
    }
}