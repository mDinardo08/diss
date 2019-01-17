import { Component, OnInit, ViewChild, ViewContainerRef } from "@angular/core";
import { GameService } from "../../services";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";

@Component({
    selector: "game",
    templateUrl: "./game.component.html"
})

export class GameComponent implements OnInit {

    constructor(private gameService: GameService) {}

    public board: BoardGame;

    public ngOnInit(): void {
        this.gameService.createGame(3).subscribe((res) => {
            this.board = res;
        }, (err) => {} );
    }

    public moveMade($event): void {

    }
}
