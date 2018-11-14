import Vue from 'vue'
import Router from 'vue-router'
import bus from "./bus"
import UserChangePwd from './pages/user/changepwd'
import UserProfile from './pages/user/profile'
import util from './common/util'
import NotFound from "./pages/notfound"
import UnAuth from "./pages/unauth"
Vue.use(Router)

let sysComps = {};

let router = new Router({
  // mode: 'history',

});

//使用此语法可以分块打包以动态加载模块
// sysComps['/index'] = resolve=> require(['./pages/index'], resolve);

//首页
import Home from './pages/index'

sysComps['/index'] = Home;
sysComps['/user/changepwd'] = UserChangePwd;
sysComps['/user/profile'] = UserProfile;


import SysDepts from './pages/sys/depts'
import SysDicts from './pages/sys/dicts'
import SysRoles from './pages/sys/roles'
import SysUers from './pages/sys/users'
import SysLog from './pages/sys/log'
import SysMenus from './pages/sys/menus'

//系统管理
sysComps['/sys/depts'] = SysDepts
sysComps['/sys/dicts'] = SysDicts
sysComps['/sys/roles'] = SysRoles
sysComps['/sys/users'] = SysUers
sysComps['/sys/log'] = SysLog
sysComps['/sys/menus'] = SysMenus

//供项目注入自己的页面,只能在路由生成之前调用
function addPage(url, pathFunc) {
  url = getComponentPath(url);
  sysComps[url] = pathFunc;
}

//获取去掉参数后组件的加载路径
function getComponentPath(url) {
  if (!url.startsWith('/')) {
    url = '/' + url;
  }
  url = url.toLowerCase().split('/:')[0];
  url = url.split('/?')[0];
  url = url.split('?')[0];

  if (url == '/') url = '/index'; //默认首页是 ./pages/index.vue
  return url;
}

// function getShortPath(cpath) {
//   cpath = cpath + "/";
//   for (var path in sysComps) {
//     if (cpath.startsWith(path + '/'))
//       return path;
//   }
//   return null;
// }

let routeCreated = false;
let userRoutes = [];
let authedIds = [];
//根据用户菜单配置生成路由信息
//因为Vue-router不支持动态清空路由，所以这里可能会报路由重得添加的错
function createRoutes(menus) {
  authedIds = menus.map(m => m.id);
  menus.forEach(menu => {
    menu.url = menu.url ||'';
    //如果是绝对路径，则忽略
    if (menu.url.indexOf('//') >= 0) {
      return;
    }

    var cpath = getComponentPath(menu.url);

    //解决某些带参数的路由，后台配置成具体的参数而形成的路由
    //比如 /sys/users/123
    if (!sysComps[cpath]) {
      // cpath = getShortPath(cpath);
      // if (!cpath)
      return;
    }

    //已经添加过的路由, 就不再添加，免得报错。
    //因为vue-router不支持清空路由重新添加
    var exists = userRoutes.find(ur => ur.meta.id == menu.id);
    if (exists) {
      return;
    }

    var rtr = {
      path: menu.url,
      component: sysComps[cpath],
      name: menu.name,
      props: true, //能通过URL传参给组件的props
      meta: {
        id: menu.id,
      }
    }
    userRoutes.push(rtr);
    router.addRoutes([rtr]);
  });
  if (routeCreated) return;
  routeCreated = true;
  router.addRoutes([{
    path: '/unauth',
    name: '没有权限',
    component: UnAuth
  }, 
  //notfound应该加在最后面，否则个个页面都会notfound
  {
    path: '*',
    name: '没有找到资源',
    component: NotFound
  }]);
}

router.beforeEach((to, from, next) => {
  if (!to.meta.id  || authedIds.includes(to.meta.id)) {
    next();
  } else {
    next('/unauth');
  }
});

export default {
  router,
  addPage,
  createRoutes
}
