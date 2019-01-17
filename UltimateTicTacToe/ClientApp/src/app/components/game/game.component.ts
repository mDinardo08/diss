import { Component, OnInit, ViewChild, ViewContainerRef } from "@angular/core";
import { GameService } from "../../services";
import { BoardGameFactory } from "../../logicComponents/boardgameFactory/boardGame.factory";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";

@Component({
    selector: "game",
    templateUrl: "./game.component.html"
})

export class GameComponent implements OnInit {

    constructor(private gameService: GameService, private gameFactory: BoardGameFactory) {}

    public board: BoardGame;

    public ngOnInit(): void {
        this.gameService.createGame(3).subscribe((res) => {
            this.board = res;
        }, (err) => {} );
    }

    public moveMade($event): void {

    }
}
