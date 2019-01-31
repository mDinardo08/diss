import { Component, OnInit } from "@angular/core";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Move } from "../../models/move/move.model";
import { AbstractGameService } from "../../services/game/game.service.abstract";
import { Player } from "../../models/player/player.model";
import { PlayerType } from "../../models/player/player.type.enum";
import { PlayerColour } from "../../models/player/player.colour.enum";
import { BsModalService } from "ngx-bootstrap/modal/bs-modal.service";
import { GameSetupComponent } from "../gameSetup/setup.component";
import { ModalOptions, BsModalRef } from "ngx-bootstrap/modal";
import { GameOverComponent } from "../gameOver/gameover.component";

@Component({
    selector: "game",
    templateUrl: "./game.component.html",
    styleUrls: ["./game.component.styles.css"]
})

export class GameComponent implements OnInit {

    constructor(private gameService: AbstractGameService, private modalService: BsModalService) {}

    public game: BoardGame;
    public availableMoves: Array<Move>;
    public lastMove: Move;
    public overlayVisable: boolean;
    public gameStarter: BsModalRef;
    public gameOver: BsModalRef;
    public players: Array<Player>;
    public ngOnInit(): void {
        this.game = new BoardGame();
        this.availableMoves = new Array<Move>();
        this.lastMove = new Move();
        const config: ModalOptions = { class: "modal-sm" };
        this.gameStarter = this.modalService.show(GameSetupComponent, config);
        this.gameStarter.content.opponentSelectedEvent.subscribe((opp) => {
            this.startGame(opp);
        });
        this.gameService.gameOverEvent.subscribe((winner) => {
            this.gameOver = this.modalService.show(GameOverComponent);
            this.gameOver.content.Winner = winner;
            this.gameOver.content.playAgainEvent.subscribe((playAgain) => {
                this.playAgain(playAgain);
            });
        });
        this.gameService.boardUpdatedEvent.subscribe((res: Array<Array<BoardGame>>) => {
            this.boardUpdated();
        });
    }
    playAgain(playAgain: boolean): void {
        if (playAgain) {
            this.overlayVisable = true;
            this.gameService.createGame(3, this.gameService.getPlayers());
        } else {
            this.gameStarter = this.modalService.show(GameSetupComponent, {class: "modal-sm"});
            this.gameStarter.content.opponentSelectedEvent.subscribe((opp) => {
                this.startGame(opp);
            });
        }
        this.gameOver.hide();
    }

    public moveMade($event: Move): void {
        this.overlayVisable = true;
        this.gameService.makeMove($event);
    }

    private startGame(opponent: Player): void {
        const human = new Player();
        human.type = PlayerType.HUMAN;
        human.colour = PlayerColour.BLUE;
        human.name = "";
        this.gameService.createGame(3, [human, opponent]);
        this.gameStarter.hide();
    }

    private boardUpdated(): void {
        this.game.board = this.gameService.getBoard();
        this.availableMoves = this.gameService.getAvailableMoves();
        this.lastMove = this.gameService.getLastMove();
        this.overlayVisable = false;
    }
}
