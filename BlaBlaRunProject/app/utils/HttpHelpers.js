"use strict";
var http_1 = require('@angular/http');
var Observable_1 = require('rxjs/Observable');
require('rxjs/Rx');
var HttpHelpers = (function () {
    function HttpHelpers(_http) {
        this._http = _http;
    }
    Object.defineProperty(HttpHelpers.prototype, "haserror", {
        get: function () {
            return this.errormsg != null;
        },
        enumerable: true,
        configurable: true
    });
    HttpHelpers.prototype.getaction = function (path) {
        return this._http.get(path)
            .map(function (res) {
            var result = res.json();
            return result;
        })
            .catch(this._handleError);
    };
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
    HttpHelpers.prototype.postRequest = function (url, data) {
        var _body = JSON.stringify(data);
        var headers = new http_1.Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        var requestoptions = new http_1.RequestOptions({
            method: http_1.RequestMethod.Post,
            url: url,
            headers: headers,
            body: JSON.stringify(data)
        });
        return this._http.request(new http_1.Request(requestoptions))
            .map(function (res) {
            if (res) {
                return [{ status: res.status, json: res.json() }];
            }
        })
            .catch(this._handleError);
    };
    HttpHelpers.prototype._handleError = function (error) {
        return Observable_1.Observable.throw(error.text() || 'Server error');
    };
    return HttpHelpers;
}());
exports.HttpHelpers = HttpHelpers;
//# sourceMappingURL=HttpHelpers.js.map