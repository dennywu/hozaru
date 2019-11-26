export default {
    header: {
        self: {},
        items: [
        ]
    },
    aside: {
        self: {},
        items: [
            {
                title: "Dashboard",
                root: true,
                icon: "flaticon2-architecture-and-city",
                page: "dashboard",
                translate: "MENU.DASHBOARD",
                bullet: "dot"
            },
            { section: "Orderan" },
            {
                title: "Orderan",
                root: true,
                bullet: "dot",
                icon: "flaticon2-browser-2",
                page: "orders"
            },
            { section: "Produk" },
            {
                title: "Product Saya",
                root: true,
                bullet: "dot",
                icon: "flaticon2-browser-2",
                page: "products"
            },
            {
                title: "Tambah Produk Baru",
                root: true,
                bullet: "dot",
                icon: "flaticon2-browser-2",
                page: "products/new",
                hide: true
            },
            {
                title: "Ubah Product",
                page: "/products/:id/edit",
                hide: true
            }
        ]
    }
};
