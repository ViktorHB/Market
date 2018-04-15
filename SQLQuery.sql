use [Data]
select * from market.Catalog
select * from market.Product
insert into market.Product (id, id_catalog, title, url, price) values (1, 1, 'Samsung S9', 'https://cdn.vox-cdn.com/thumbor/309v_5GNMQnzola5LtOpFjB7w4w=/0x0:3960x2640/1200x800/filters:focal(1664x1004:2296x1636)/cdn.vox-cdn.com/uploads/chorus_image/image/58464691/samsunggalaxys9leak.1516980557.jpg', 1000),
(2, 1, 'Iphone X', 'https://store.storeimages.cdn-apple.com/4974/as-images.apple.com/is/image/AppleInc/aos/published/images/i/ph/iphone/x/iphone-x-gray-select-2017?wid=1200&hei=630&fmt=jpeg&qlt=95&op_sharpen=0&resMode=bicub&op_usm=0.5,0.5,0,0&iccEmbed=0&layer=comp&.v=1515602510330', 1200),
  (3, 1, 'OnePlus 5', 'https://images.kogan.com/image/fetch/s--qQcqJQCi--/b_white,c_pad,f_auto,h_400,q_auto:good,w_600/https://assets.kogan.com/files/product/HKI/KHOP564GRY_1v2.jpg', 1000),
  insert into market.Product (id, id_catalog, title, url, price) values (4, 2, 'Xiaomi MiNoteBook', 'https://zdnet3.cbsistatic.com/hub/i/r/2018/02/01/4fff202e-2283-470d-9278-298195ae0e51/thumbnail/770x433/35fda64899ffd2b5532ac17207dbd5c7/xiaomi-mi-nb-pro-header.jpg', 1500),
  (5, 2, 'MacBook Pro 15', 'https://www.bhphotovideo.com/images/images2500x2500/apple_z0sg_mlh321_bh_macbook_pro_15_inch_with_1294011.jpg', 2000),
  (6, 2, 'Hp S5612s', 'http://ssl-product-images.www8-hp.com/digmedialib/prodimg/lowres/c04818026.png', 1000),
  (7, 3, 'Ipad Pro', 'https://store.storeimages.cdn-apple.com/4974/as-images.apple.com/is/image/AppleInc/aos/published/images/i/pa/ipad/pro/ipad-pro-10in-wifi-select-spacegray-201706?wid=470&hei=556&fmt=png-alpha&qlt=95&.v=1505500512829', 1200),
  (8, 3, 'Samsung Galaxy Tab 2', 'https://static.svyaznoy.ru/upload/iblock/683/267530_1_a.jpg/resize/483x483/hq/', 500),
  (9, 3, 'Microsoft Surface', 'https://c.s-microsoft.com/en-us/CMSImages/SurfaceHome_Book2_1_ContentPlacement1_V1.png.jpg?version=a9a03fa5-983a-62c9-40eb-78bc232f25b3', 1500)


 select * from market.Orders
  