import { Component, OnInit, AfterViewInit } from "@angular/core";
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
import { UserService } from "../../services";
import { ToasterService } from "angular2-toaster";

@Component({
    selector: "game",
    templateUrl: "./game.component.html",
    styleUrls: ["./game.component.styles.css"]
})

export class GameComponent implements OnInit, AfterViewInit {

    constructor(private gameService: AbstractGameService, private modalService: BsModalService, private userService: UserService,
         private toast: ToasterService) {}

    public game: BoardGame;
    public availableMoves: Array<Move>;
    public lastMove: Move;
    public overlayVisable: boolean;
    public gameStarter: BsModalRef;
    public gameOver: BsModalRef;
    public players: Array<Player>;
    public noGames: number;

    public ngAfterViewInit(): void {
        const  user = this.userService.getUserId();
        if (user === -1) {
            this.toast.pop("warning", "not logged in: Some Ai may not perform optimally");
        } else {
            this.toast.pop("success", "Logged in as " + this.userService.getUserId());
        }
    }

    public ngOnInit(): void {
        this.game = new BoardGame();
        this.availableMoves = new Array<Move>();
        this.lastMove = new Move();
        this.gameStarter = this.modalService.show(GameSetupComponent, {class: "modal-lg"});
        this.gameStarter.content.opponentSelectedEvent.subscribe((opp: Array<Player>) => {
            this.startGame(opp);
        });
        this.gameService.gameOverEvent.subscribe((winner) => {
            this.noGames--;
            if (this.noGames > 0) {
                const players = this.gameService.getPlayers().reverse();
                this.gameService.createGame(3, players);
            } else {
                this.gameOver = this.modalService.show(GameOverComponent);
                this.gameOver.content.Winner = winner;
                this.gameOver.content.playAgainEvent.subscribe((playAgain) => {
                this.playAgain(playAgain);
            });
            }
        });
        this.gameService.boardUpdatedEvent.subscribe((res: Array<Array<BoardGame>>) => {
            this.boardUpdated(res);
        });
    }
    playAgain(playAgain: boolean): void {
        if (playAgain) {
            this.overlayVisable = true;
            this.gameService.createGame(3, this.gameService.getPlayers());
        } else {
            this.gameStarter = this.modalService.show(GameSetupComponent, {class: "modal-lg"});
            this.gameStarter.content.opponentSelectedEvent.subscribe((players) => {
                this.startGame(players);
            });
        }
        this.gameOver.hide();
    }

    public moveMade($event: Move): void {
        this.overlayVisable = true;
        this.gameService.makeMove($event);
    }

    private startGame(players: Array<Player>): void {
        this.noGames = this.gameStarter.content.getNoGames();
        this.gameService.createGame(3, players);
        this.gameStarter.hide();
    }

    private boardUpdated(res: Array<Array<BoardGame>>): void {
        this.game.board = res;
        this.availableMoves = this.gameService.getAvailableMoves();
        this.lastMove = this.gameService.getLastMove();
        this.overlayVisable = false;
    }
}
