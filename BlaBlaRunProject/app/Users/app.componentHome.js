"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
//import { Router } from '@angular/router';
require('rxjs/add/operator/map');
var HttpHelpers_1 = require('Utils/HttpHelpers');
var HomeListComponent = (function (_super) {
    __extends(HomeListComponent, _super);
    function HomeListComponent(http, eltRef, mTableName, mOrderColumn, mWaitingTime) {
        _super.call(this, http);
        this.http = http;
        this.eltRef = eltRef;
        this.list = [];
        this.columnNames = [];
        var native = this.eltRef.nativeElement;
        this.homeWindowRefreshInterval = native.getAttribute("homeWindowRefreshInterval");
        this.clientUrl = native.getAttribute("clientUrl");
        this.columnNames = native.getAttribute("columnNames").trim().split(",");
        this.itemSelectecUrl = native.getAttribute("itemSelectecUrl");
        this.lastStatus = "Last updated: " + new Date().toUTCString();
        this.tableName = mTableName;
        this.orderColumn = mOrderColumn;
        this.waitingTime = mWaitingTime;
        //console.log('clientUrl: ' + this.clientUrl);
        //Observable.interval(this.homeWindowRefreshInterval * 60 * 1000)
        //    .subscribe(() => {
        //        setTimeout(() => {
        //            this.refreshData();
        //        }, this.waitingTime);
        //    });
        this.refreshData();
    }
    HomeListComponent.prototype.refreshData = function () {
        this.getData();
    };
    HomeListComponent.prototype.getData = function () {
        var _this = this;
        //console.log('getData start'); 
        this.getaction(this.clientUrl).subscribe(function (result) {
            if (_this.dataTable) {
                _this.dataTable.destroy();
            }
            _this.list = result;
            setTimeout(function () {
                _this.createDataTable();
                _this.lastStatus = "Last updated: " + new Date().toUTCString();
            }, 1000);
        }, function (error) { return _this.errormsg = error; });
    };
    HomeListComponent.prototype.createDataTable = function () {
        // run jQuery stuff here
        this.dataTable = $(this.tableName).dataTable({
            destroy: true,
            "dom": 'lfr<"toolbar">tip',
            stateSave: true,
            "lengthMenu": [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
            "order": [[this.orderColumn, "desc"]],
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }
            ]
        });
    };
    __decorate([
        core_1.Input('homeWindowRefreshInterval'), 
        __metadata('design:type', Number)
    ], HomeListComponent.prototype, "homeWindowRefreshInterval", void 0);
    __decorate([
        core_1.Input('clientUrl'), 
        __metadata('design:type', String)
    ], HomeListComponent.prototype, "clientUrl", void 0);
    __decorate([
        core_1.Input('columnNames'), 
        __metadata('design:type', Array)
    ], HomeListComponent.prototype, "columnNames", void 0);
    __decorate([
        core_1.Input('itemSelectecUrl'), 
        __metadata('design:type', String)
    ], HomeListComponent.prototype, "itemSelectecUrl", void 0);
    return HomeListComponent;
}(HttpHelpers_1.HttpHelpers));
exports.HomeListComponent = HomeListComponent;
//# sourceMappingURL=app.componentHome.js.map