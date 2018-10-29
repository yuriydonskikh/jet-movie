import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, XHRBackend } from '@angular/http';
import { AuthenticateXHRBackend } from './authenticate-xhr.backend';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { routing } from './app.routing';

/* App Root */
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';

/* Account Imports */
import { AccountModule } from './account/account.module';
/* Movies Imports */
import { MoviesModule } from './movies/movies.module';

import { ConfigService } from './shared/utils/config.service';

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        HomeComponent
    ],
    imports: [
        AccountModule,
        MoviesModule,
        BrowserModule,
        FormsModule,
        HttpModule,
        InfiniteScrollModule,
        FontAwesomeModule,
        routing
    ],
    providers: [
        ConfigService, {
            provide: XHRBackend,
            useClass: AuthenticateXHRBackend
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
