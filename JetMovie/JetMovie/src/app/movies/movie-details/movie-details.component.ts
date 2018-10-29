import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MoviesService } from '../services/movies.service';
import { CartService } from '../services/cart.service';
import { MovieViewModel } from '../models/movie-view-model';
import { CartViewModel } from '../models/cart-view-model';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faVideo, faGlobe, faCartArrowDown } from '@fortawesome/free-solid-svg-icons'
library.add(faVideo, faGlobe, faCartArrowDown);

@Component({
    selector: 'app-movie-details',
    templateUrl: './movie-details.component.html',
    styleUrls: ['./movie-details.component.scss']
})
export class MovieDetailsComponent implements OnInit {

    isRequesting: boolean;
    movie: MovieViewModel;
    addedToCart: boolean = false;

    constructor(private moviesService: MoviesService, private cartService: CartService, private router: Router, private activatedRout: ActivatedRoute) { }

    ngOnInit() {
        this.isRequesting = true;
        this.activatedRout.params.subscribe(
            params => {
                this.moviesService.getMovie(params['id'])
                    .finally(() => this.isRequesting = false)
                    .subscribe((movie: MovieViewModel) => {
                        this.movie = movie;
                    },
                        () => {
                            this.router.navigate(["/movies"]);
                        });
            });
    }

    addToCart() {
        this.isRequesting = true;

        let cartvm = new CartViewModel();
        cartvm.movieId = this.movie.id;
        cartvm.providedBy = this.movie.providedBy;
        cartvm.price = this.movie.price;

        this.cartService.addCartItem(cartvm)
            .finally(() => this.isRequesting = false)
            .subscribe(result => {
                if (result) this.addedToCart = true;
            },
                error => {
                    //this.notificationService.printErrorMessage(error);
                });
    }

}
