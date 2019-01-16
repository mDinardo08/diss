import { Component, OnInit, ViewChild, ViewContainerRef } from "@angular/core";
import { GameService } from "../../services";
import { BoardGameFactory } from "../../logicComponents/boardgameFactory/boardGame.factory";
import { EMLINK } from "constants";
import { element } from "protractor";
import { TictactoeComponent } from "../ultimateTictactoe/ultimateTictactoe.component";

@Component({
    selector: "game",
    templateUrl: "./game.component.html"
})

export class GameComponent implements OnInit {

    @ViewChild("gameView", {read: ViewContainerRef}) gameView: TictactoeComponent;

    constructor(private gameService: GameService, private gameFactory: BoardGameFactory) {}

    public ngOnInit(): void {
        this.gameService.createGame(3).subscribe((res) => {
        }, (err) => {} );
    }

}
