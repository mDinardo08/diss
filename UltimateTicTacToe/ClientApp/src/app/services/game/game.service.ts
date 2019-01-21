import { Injectable } from "@angular/core";
import { ApiService } from "../api/api.service";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
import { Observable } from "rxjs/Observable";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardCreationDTO } from "../../models/DTOs/BoardCreationDTO";
import { Player } from "../../models/player/player.model";

@Injectable()
export class GameService {

    private curPlayer: Player;

    constructor(private api: ApiService) {}

    makeMove(game: Array<Array<BoardGame>>): Observable<BoardGameDTO> {
        return this.api.post<BoardGameDTO>("Game/makeMove", game);
    }

    createGame(size: number, players: Array<Player>): Observable<BoardGameDTO> {
        const creationDto = new BoardCreationDTO();
        creationDto.size = size;
        creationDto.players = players;
        const dto = this.api.post<BoardGameDTO>("Game/createBoard", creationDto);
        dto.subscribe((res) => {
            this.curPlayer = res.cur;
            console.log("service");
        });
        return dto;
    }

    getCurrentPlayer(): Player {
        return this.curPlayer;
    }
}
