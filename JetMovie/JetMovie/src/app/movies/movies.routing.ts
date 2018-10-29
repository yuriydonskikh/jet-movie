import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { RootComponent } from './root/root.component';
import { HomeComponent } from './home/home.component';
import { MovieDetailsComponent } from "./movie-details/movie-details.component";
import { CartComponent } from "./cart/cart.component";

import { AuthGuard } from '../auth.guard';

export const routing: ModuleWithProviders = RouterModule.forChild([
    {
        path: 'movies',
        component: RootComponent,
        canActivate: [AuthGuard],

        children: [
            { path: '', component: HomeComponent },
            { path: 'home', component: HomeComponent },
            { path: 'details/:id', component: MovieDetailsComponent },
            { path: 'cart', component: CartComponent },
        ]
    }
]);

