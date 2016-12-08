import { Component, OnInit, Input, ElementRef } from '@angular/core';
import { RequestOptions, Http, Response, Headers, URLSearchParams, HttpModule } from '@angular/http';

import { HomeListComponent } from './app.componentHome'

@Component({
    selector: 'app-Users-List',
    templateUrl: './Users/UsersList'
})
export class UsersList extends HomeListComponent<BlaBlaRunProject.Domain.Concrete.Users>
{

    constructor(private mhttp: Http, private meltRef: ElementRef) {
        super(mhttp, meltRef, '#tableUsers', 0, 0);

        //console.log('clientUrl: ' + this.clientUrl);

    }
}
