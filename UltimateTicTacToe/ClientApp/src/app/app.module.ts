import { BrowserModule } from "@angular/platform-browser";
import { NgModule, ComponentFactoryResolver } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import * as Components from "./components";
import { AppComponent } from "./app.component";
import * as Services from "./services";
@NgModule({
  declarations: [
    AppComponent,
    Components.NavMenuComponent,
    Components.GameComponent,
    Components.TictactoeComponent,
    Components.TileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: Components.GameComponent, pathMatch: "full" }
    ])
  ],
  providers: [Services.ApiService, { provide: Services.AbstractGameService, useClass: Services.GameService }],
  bootstrap: [AppComponent]
})
export class AppModule { }
