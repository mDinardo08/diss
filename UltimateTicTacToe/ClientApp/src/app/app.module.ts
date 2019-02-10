import { BrowserModule } from "@angular/platform-browser";
import { NgModule, ComponentFactoryResolver } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import * as Components from "./components";
import { AppComponent } from "./app.component";
import * as Services from "./services";
import { ModalModule } from "ngx-bootstrap/modal";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastrModule, ToastrService } from "ngx-toastr";
import { RoutingModule } from "./routing.module";
@NgModule({
  declarations: [
    AppComponent,
    Components.NavMenuComponent,
    Components.GameComponent,
    Components.TictactoeComponent,
    Components.TileComponent,
    Components.GameSetupComponent,
    Components.GameOverComponent,
    Components.UserComponent
  ],
  imports: [
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RoutingModule
  ],
  entryComponents: [Components.GameSetupComponent, Components.GameOverComponent],
  providers: [Services.ApiService, { provide: Services.AbstractGameService, useClass: Services.GameService }, Services.UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
