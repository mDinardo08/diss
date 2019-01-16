import { BrowserModule } from "@angular/platform-browser";
import { NgModule, ComponentFactoryResolver } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import * as Components from "./components";
import { AppComponent } from "./app.component";
import * as Services from "./services";
import * as Logic from "./logicComponents";
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
  providers: [Services.ApiService, Services.GameService, Logic.BoardGameFactory],
  bootstrap: [AppComponent],
  entryComponents: [Components.TictactoeComponent]
})
export class AppModule { }
