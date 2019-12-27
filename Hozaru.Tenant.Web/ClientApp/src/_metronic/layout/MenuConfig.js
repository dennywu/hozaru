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
            { section: "Pesanan" },
            {
                title: "Pesanan",
                root: true,
                bullet: "dot",
                icon: "flaticon-file-2",
                page: "orders"
            }, 
            {
                title: "Rincian Pesanan",
                page: "/orders/:id/detail",
                hide: true
            },
            { section: "Produk" },
            {
                title: "Product Saya",
                root: true,
                bullet: "dot",
                icon: "flaticon2-tag",
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
            },
            {
                section: "Pengaturan"
            },
            {
                title: "Pengaturan Toko",
                icon: "flaticon2-gear",
                page: "setting-tenant"
            },
            {
                title: "Metode Pembayaran",
                icon: "flaticon-price-tag",
                page: "paymentmethods"
            },
            {
                title: "Tambah Metode Pembayaran",
                page: "paymentmethods/new",
                hide: true
            },
            {
                title: "Ubah Metode Pembayaran",
                page: "/paymentmethods/:id/edit",
                hide: true
            },
            {
                title: "Jasa Pengiriman",
                icon: "flaticon-truck",
                page: "settingexpeditionservices"
            }
        ]
    }
};
