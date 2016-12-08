import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

//import { AppComponent } from './app.component';
import { UsersList} from './app.componentUsersList'

@NgModule({
    imports: [BrowserModule, HttpModule ],
  declarations: [UsersList ],
  bootstrap: [UsersList ]
})
export class AppModule { }
