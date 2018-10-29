import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faFilter } from '@fortawesome/free-solid-svg-icons'
library.add(faFilter);

import { MoviesService } from '../services/movies.service';
import { MovieRequest, SortOptions } from "../models/movie-request"
import { MovieViewModel, ProvidedBy } from "../models/movie-view-model"

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

    movies: Array<MovieViewModel>;
    currentRequest: MovieRequest;
    modalOpen = false;
    isRequesting: boolean;

    constructor(private moviesService: MoviesService, private router: Router) { }

    ngOnInit() {
        this.currentRequest = new MovieRequest;
        this.currentRequest.PageSize = 20;
        this.currentRequest.Page = 1;
        this.currentRequest.SortBy = SortOptions.ByDateDesc;
        this.movies = new Array<MovieViewModel>();
        this.getMovies();
    }

    getMovies() {
        this.isRequesting = true;
        this.moviesService.getMovies(this.currentRequest, "MovieInfo")
            .finally(() => this.isRequesting = false)
            .subscribe((movies: Array<MovieViewModel>) => {
                if (movies != undefined) {
                    movies.forEach(item => { this.movies.push(item) });
                }
            },
                error => {
                    //this.notificationService.printErrorMessage(error);
                });
    }

    onScroll() {
        if (!this.isRequesting) {
            this.currentRequest.Page++;
            this.getMovies();
        }
    }

    onFilter({ value, valid }: { value: MovieRequest, valid: boolean }) {
        if (valid) {
            console.log(value);
            this.currentRequest = value;
            this.currentRequest.Page = 1;
            this.currentRequest.PageSize = 20;
            this.movies = new Array<MovieViewModel>();
            this.getMovies();
        }
    }

    toMovieDetails(id: string) {
        this.router.navigate(['/movies/details/' + id]);
    }
}
