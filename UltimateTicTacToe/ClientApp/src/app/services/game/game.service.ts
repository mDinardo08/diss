import { Injectable, Output, EventEmitter } from "@angular/core";
import { ApiService } from "../api/api.service";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
import { Observable } from "rxjs/Observable";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardCreationDTO } from "../../models/DTOs/BoardCreationDTO";
import { Player } from "../../models/player/player.model";
import { Move } from "../../models/move/move.model";
import { AbstractGameService } from "./game.service.abstract";
import { RatingDTO } from "../../models/DTOs/RatingDTO";
import { MoveDTO } from "../../models/DTOs/MoveDTO";
import { PlayerType } from "../../models/player/player.type.enum";

@Injectable()
export class GameService extends AbstractGameService {

    curPlayer: Player;
    players: Array<Player>;
    board: Array<Array<BoardGame>>;
    availableMoves: Array<Move>;
    lastMove: Move;
    playerRatings: Array<Array<number>>;
    playerLowOptions: Array<Array<number>>;
    playerHighOptions: Array<Array<number>>;
    constructor(private api: ApiService) {
        super();
    }

    getLastMove(): Move {
        return this.lastMove;
    }

    makeMove(move: Move): void {
        if (this.curPlayer.type === PlayerType.HUMAN) {
            this.handleMove(move);
        }
    }

    createGame(size: number, players: Array<Player>): Observable<BoardGameDTO> {
        this.playerRatings = new Array<Array<number>>();
        this.playerHighOptions = new Array<Array<number>>();
        this.playerLowOptions = new Array<Array<number>>();
        players.forEach(player => {
            this.playerRatings.push(new Array<number>());
            this.playerHighOptions.push(new Array<number>());
            this.playerLowOptions.push(new Array<number>());
        });
        const creationDto = new BoardCreationDTO();
        creationDto.size = size;
        creationDto.players = players;
        this.players = players;
        this.lastMove = null;
        const dto = this.api.post<BoardGameDTO>("Game/createBoard", creationDto);
        dto.subscribe((res) => {
            this.players = res.players;
            this.boardUpdated(res);
        });
        return dto;
    }

    getCurrentPlayer(): Player {
        return this.curPlayer;
    }

    getAvailableMoves(): Move[] {
        return this.availableMoves;
    }

    getBoard(): Array<Array<BoardGame>> {
        return this.board;
    }

    getPlayers(): Player[] {
        return this.players;
    }
    makeMoveOnBoard(board: Array<Array<BoardGame>>, move: Move): Array<Array<BoardGame>> {
        const result = board;
        const point = move.possition;
        if (move.next.possition === null || move.next.possition === undefined) {
            result[point.x][point.y].owner = move.owner;
        } else {
            result[point.x][point.y].board = this.makeMoveOnBoard(result[point.x][point.y].board, move.next);
        }
        return result;
    }

    getNextPlayer(): Player {
        return this.players.find(x => x.colour !== this.curPlayer.colour);
    }

    boardUpdated(res: BoardGameDTO): void {
        this.curPlayer = res.cur;
        //console.log("Player was: " + this.getNextPlayer().name + " player: " + this.players.findIndex(x => x === this.getNextPlayer()) +
        //    " move was rated: " + res.lastMoveRating);
        this.board = res.game;
        this.availableMoves = res.availableMoves;
        if (res.lastMove !== undefined && res.lastMove !== null) {
            this.lastMove = res.lastMove;
        }
        if ((res.winner !== undefined && res.winner !== null) ||
            this.availableMoves.length === 0) {
            const winner = this.players.find(x => x.colour === res.winner);
            if (winner !== undefined) {
                console.log("Game Over Winner is :" + winner.name);
            } else {
                console.log("Game Over it was a tie");
            }
            const curIndex = this.players.findIndex(x => x.colour !== this.curPlayer.colour);
            this.playerRatings[curIndex].push(res.lastMoveRating);
            for (let x = 0; x < this.playerRatings.length; x++) {
                console.log("Player: " + this.players[x].name);
                console.log("scores: ");
                let results = "";
                this.playerRatings[x].forEach(rating => {
                    results += rating + ", ";
                });
                console.log(results);
                console.log("Highest possible scores: ");
                results = "";
                this.playerHighOptions[x].forEach(option => {
                    results += option + ", ";
                });
                console.log(results);
                console.log("Lowest possible scores: ");
                results = "";
                this.playerLowOptions[x].forEach(option => {
                    results += option + ", ";
                });
                console.log(results);
            }
            this.gameOverEvent.emit(res.winner);
        } else {
            this.boardUpdatedEvent.emit(this.board);
            if (this.curPlayer.type !== PlayerType.HUMAN) {
                const curIndex = this.players.findIndex(x => x.colour !== this.curPlayer.colour);
                this.playerRatings[curIndex].push(res.lastMoveRating);
                this.playerHighOptions[curIndex].push(res.highOption);
                this.playerLowOptions[curIndex].push(res.lowOption);
                this.handleMove(this.lastMove);
            }
        }
    }

    handleMove(move: Move): void {
        if (this.curPlayer.type === PlayerType.HUMAN) {
            this.rateMove(move);
        }
        this.sendMoveToServer(move);
    }

    rateMove(move: Move): void {
        const moveDto = new MoveDTO();
        moveDto.game = this.board;
        moveDto.lastMove = this.lastMove;
        moveDto.move = move;
        moveDto.UserId = this.curPlayer.userId;
        this.api.post<RatingDTO>("Game/rateMove", moveDto).subscribe((res) => {
            console.log(res);
            const curIndex = this.players.findIndex(x => x.userId === res.userId);
            this.playerRatings[curIndex].push(res.latest);
            this.playerHighOptions[curIndex].push(res.highOption);
            this.playerLowOptions[curIndex].push(res.lowOption);
        });
    }

    sendMoveToServer(move: Move): void {
        const Dto = new BoardGameDTO();
        Dto.game = this.makeMoveOnBoard(this.board, move);
        this.lastMove = move;
        Dto.lastMove = move;
        Dto.players = this.players;
        Dto.cur = this.curPlayer.type === PlayerType.HUMAN ? this.getNextPlayer() : this.curPlayer;
        const result = this.api.post<BoardGameDTO>("Game/makeMove", Dto);
        result.subscribe((res) => {
            this.boardUpdated(res);
        });
    }
}
