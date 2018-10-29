import { Component, OnInit } from '@angular/core';
import { CartService } from "../services/cart.service";
import { CartViewModel } from "../models/cart-view-model";
import { Router } from '@angular/router';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faShoppingCart, faVideo, faGlobe, faTrash } from '@fortawesome/free-solid-svg-icons'
library.add(faShoppingCart, faVideo, faGlobe, faTrash);

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

    cart: Array<CartViewModel>;
    calcPrice: number = 0;
    constructor(private cartService: CartService, private router: Router) { }

    ngOnInit() {
        this.refreshCart();
    }

    refreshCart() {
        this.cartService.getCartItems()
            .subscribe((items: Array<CartViewModel>) => {
                this.cart = items;
                this.calcTotal();
            });
    }

    calcTotal() {
        this.calcPrice = 0;
        for (let item of this.cart) {
            this.calcPrice += item.price;
        }
    }

    deleteItem(id: number) {
        this.cartService.deleteCartItem(id)
            .subscribe(() => {
                this.refreshCart();
            });
    }

    pay() {
        let success = true;
        for (let item of this.cart) {
            this.cartService.setPaid(item.id)
                .subscribe(() => {

                },
                    () => {
                        success = false;
                    });
        }

        this.refreshCart();

        if (success) {
            this.router.navigate(['/movies']);
        }
    }

}
