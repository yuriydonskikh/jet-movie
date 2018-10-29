import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/modules/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { routing } from './movies.routing';
import { RootComponent } from './root/root.component';
import { HomeComponent } from './home/home.component';
import { MoviesService } from './services/movies.service';
import { CartService } from './services/cart.service';

import { AuthGuard } from '../auth.guard';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { CartComponent } from './cart/cart.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        routing,
        SharedModule,
        InfiniteScrollModule,
        FontAwesomeModule
    ],
    declarations: [RootComponent, HomeComponent, MovieDetailsComponent, CartComponent],
    exports: [],
    providers: [AuthGuard, MoviesService, CartService]
})
export class MoviesModule { }
