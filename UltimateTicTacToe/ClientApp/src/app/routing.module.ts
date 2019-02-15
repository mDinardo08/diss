import { NgModule, Component } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import * as Components from "./components";

const appRoutes: Routes = [
  { path: "", redirectTo: "login", pathMatch: "full" },
  { path: "game", component: Components.GameComponent},
  { path: "login", component: Components.UserComponent}
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class RoutingModule {}
