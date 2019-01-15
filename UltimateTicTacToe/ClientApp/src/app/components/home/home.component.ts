import { Component, OnInit } from "@angular/core";
import { GameService } from "../../services/game/game.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
})
export class HomeComponent implements OnInit {

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    console.log("stuff");
  }

  public test(): void {
      this.gameService.createGame(3).subscribe((suc) => {
        console.log(suc);
      }, (err) => {
        console.log(err);
      });
  }
}
