import { Injectable } from "@angular/core";
import { ApiService } from "../api/api.service";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
import { Observable } from "rxjs/Observable";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardCreationDTO } from "../../models/DTOs/BoardCreationDTO";
import { Player } from "../../models/player/player.model";
import { Move } from "../../models/move/move.model";
import { IGameService } from "./game.service.interface";

@Injectable()
export class GameService implements IGameService {

    curPlayer: Player;
    players: Array<Player>;
    board: Array<Array<BoardGame>>;
    constructor(private api: ApiService) {}

    makeMove(move: Move): Observable<BoardGameDTO> {
        const Dto = new BoardGameDTO();
        Dto.game = this.makeMoveOnBoard(this.board, move);
        Dto.lastMove = move;
        Dto.players = this.players;
        Dto.cur = this.getNextPlayer()  ;
        const result = this.api.post<BoardGameDTO>("Game/makeMove", Dto);
        result.subscribe((res) => {
            this.curPlayer = res.cur;
        });
        return result;
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
        });
        return dto;
    }

    getCurrentPlayer(): Player {
        return this.curPlayer;
    }

    makeMoveOnBoard(board: Array<Array<BoardGame>>, move: Move): Array<Array<BoardGame>> {
        const result = board;
        const point = move.possition;
        if (move.next === null || move.next === undefined) {
            result[point.X][point.Y].owner = this.curPlayer;
        } else {
            result[point.X][point.Y].board = this.makeMoveOnBoard(result[point.X][point.Y].board, move.next);
        }
        return result;
    }

    getNextPlayer(): Player {
        return this.players.find(x => x.colour !== this.curPlayer.colour);
    }
}
