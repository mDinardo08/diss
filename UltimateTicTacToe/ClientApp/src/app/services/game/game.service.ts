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
            result[point.x][point.y].owner = this.curPlayer.colour;
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
        this.board = res.game;
        this.availableMoves = res.availableMoves;
        if (res.lastMove !== undefined && res.lastMove !== null) {
            this.lastMove = res.lastMove;
        }
        if (this.curPlayer.type !== PlayerType.HUMAN) {
            this.handleMove(this.lastMove);
        }
        this.boardUpdatedEvent.emit(this.board);
        if ((res.winner !== undefined && res.winner !== null) ||
            this.availableMoves.length === 0) {
            this.gameOverEvent.emit(res.winner);
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
        this.api.post<RatingDTO>("Game/RateMove", moveDto).subscribe((res) => {
            console.log(res);
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
            console.log("Player was: " + this.getNextPlayer() + "move was rated" + res.lastMoveRating);
            this.boardUpdated(res);
        });
    }
}
