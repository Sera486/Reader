import HomePage from 'components/home-page'
import LibraryPage from 'components/library-page'
import BookPage from 'components/book-page'
import Error404Page from 'components/error404-page'

export const routes = [
    {
        path: '/',
        component: HomePage,
        meta: {
            title: 'Home',
            requiresAuth: false,
        },
        display: 'Home',
        style: 'glyphicon glyphicon-home'
    },
    {
        path: '/library',
        component: LibraryPage,
        meta: {
            title: 'Library',
            requiresAuth: false,
        },
        display: 'Library',
        style: 'glyphicon glyphicon-book'
    },
    {
        path: '/book/:id',
        name: "book",
        component: BookPage,
        meta: {
            title: 'Loading...',
            requiresAuth: false,
        },
        style: 'glyphicon glyphicon-th-list'
    },
    { path: '*', component: Error404Page },
];
