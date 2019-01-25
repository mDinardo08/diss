import { Injectable, Output, EventEmitter } from "@angular/core";
import { ApiService } from "../api/api.service";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
import { Observable } from "rxjs/Observable";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardCreationDTO } from "../../models/DTOs/BoardCreationDTO";
import { Player } from "../../models/player/player.model";
import { Move } from "../../models/move/move.model";
import { AbstractGameService } from "./game.service.abstract";

@Injectable()
export class GameService extends AbstractGameService {

    curPlayer: Player;
    players: Array<Player>;
    board: Array<Array<BoardGame>>;
    availableMoves: Array<Move>;
    @Output() boardUpdatedEvent = new EventEmitter<Array<Array<BoardGame>>>();

    constructor(private api: ApiService) {
        super();
    }

    makeMove(move: Move): void {
        const Dto = new BoardGameDTO();
        Dto.game = this.makeMoveOnBoard(this.board, move);
        Dto.lastMove = move;
        Dto.players = this.players;
        Dto.cur = this.getNextPlayer()  ;
        const result = this.api.post<BoardGameDTO>("Game/makeMove", Dto);
        result.subscribe((res) => {
            this.curPlayer = res.cur;
            this.board = res.game;
            this.availableMoves = res.availableMoves;
            this.boardUpdatedEvent.emit(this.board);
        });
    }

    createGame(size: number, players: Array<Player>): Observable<BoardGameDTO> {
        const creationDto = new BoardCreationDTO();
        creationDto.size = size;
        creationDto.players = players;
        this.players = players;
        const dto = this.api.post<BoardGameDTO>("Game/createBoard", creationDto);
        dto.subscribe((res) => {
            this.curPlayer = res.cur;
            this.players = res.players;
            this.board = res.game;
            this.boardUpdatedEvent.emit(this.board);
        });
        return dto;
    }

    getCurrentPlayer(): Player {
        return this.curPlayer;
    }

    getAvailableMoves(): Move[] {
        return this.availableMoves;
    }

    makeMoveOnBoard(board: Array<Array<BoardGame>>, move: Move): Array<Array<BoardGame>> {
        const result = board;
        const point = move.possition;
        if (move.next === null || move.next === undefined) {
            result[point.x][point.y].owner = this.curPlayer;
        } else {
            result[point.x][point.y].board = this.makeMoveOnBoard(result[point.x][point.y].board, move.next);
        }
        return result;
    }

    getNextPlayer(): Player {
        return this.players.find(x => x.colour !== this.curPlayer.colour);
    }
}
