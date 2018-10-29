import { Injectable } from '@angular/core';
import { Http, Response, Headers, URLSearchParams } from '@angular/http';

import { ConfigService } from '../../shared/utils/config.service';

import { BaseService } from '../../shared/services/base.service';

import { MovieRequest } from '../models/movie-request';
import { MovieViewModel } from '../models/movie-view-model';

import { Observable } from 'rxjs/Rx';

// Add the RxJS Observable operators we need in this app.
import '../../rxjs-operators';

@Injectable()
export class MoviesService extends BaseService {

    baseUrl: string = '';

    constructor(private http: Http, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiURI();
    }

    getAuthHeader(): Headers {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let authToken = localStorage.getItem('auth_token');
        headers.append('Authorization', `Bearer ${authToken}`);
        return headers;
    }

    getMovies(request: MovieRequest, controller: string): Observable<Array<MovieViewModel>> {
        let headers = this.getAuthHeader();

        let params = new URLSearchParams();
        for (let key in request) {
            if (request.hasOwnProperty(key)) {
                params.set(key, request[key]);
            }
        }
        return this.http.get(this.baseUrl + "/" + controller + "/GetMovies?" + params.toString(), { headers })
            .map(response => response.json())
            .catch(this.handleError);
    }

    getMovie(id: string): Observable<MovieViewModel> {
        let headers = this.getAuthHeader();

        return this.http.get(this.baseUrl + "/MovieInfo/GetMovie/" + id, { headers })
            .map(response => response.json())
            .catch(this.handleError);
    }
}
