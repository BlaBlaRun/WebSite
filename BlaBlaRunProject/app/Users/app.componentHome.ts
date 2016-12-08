import { Component, OnInit, Input, ElementRef } from '@angular/core';
import { RequestOptions, Http, Response, Headers, URLSearchParams, HttpModule } from '@angular/http';
import { Observable } from 'rxjs/Rx';
//import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import { HttpHelpers } from 'Utils/HttpHelpers';

declare var $: any;


export class HomeListComponent<T> extends HttpHelpers {
    list: Array<T> = [];
    @Input('homeWindowRefreshInterval') homeWindowRefreshInterval: number;
    @Input('clientUrl') clientUrl: string;
    @Input('columnNames') columnNames: Array<string> = [];
    @Input('itemSelectecUrl') itemSelectecUrl: string;
    lastStatus: string;
    dataTable: any;
    tableName: string;
    orderColumn: number;
    waitingTime: number;

    

    constructor(private http: Http, private eltRef: ElementRef, mTableName: string, mOrderColumn: number, mWaitingTime:number) {
        super(http);
        let native = this.eltRef.nativeElement;
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
    

    refreshData() {
        this.getData();
        
    }

    getData() {
        //console.log('getData start'); 
        this.getaction<T>(this.clientUrl).subscribe(
            result => {
                if (this.dataTable) {
                    this.dataTable.destroy();
                }
                this.list = result;
                setTimeout(() => {
                    this.createDataTable();
                    this.lastStatus = "Last updated: " + new Date().toUTCString();
                }, 1000);
            },
            error => this.errormsg = error);
    }
    
    createDataTable() {
        // run jQuery stuff here


        this.dataTable = $(this.tableName).dataTable(
            {
                destroy: true,
                "dom": 'lfr<"toolbar">tip',
                stateSave: true,
                "lengthMenu": [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
                "order": [[this.orderColumn, "desc"]]
                ,
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    }
                ]
            });
    }
    

}