var scene = (function () {
    "use strict";

    var scene = new THREE.Scene();
    var renderer = new THREE.WebGLRenderer({ alpha:true });

    var camera, sphere, material, geometry, texture;

    function InitScene() {
        renderer.setSize(window.innerWidth, window.innerHeight);

        document.getElementById('container').appendChild(renderer.domElement);

        camera = new THREE.PerspectiveCamera(35, window.innerWidth / window.innerHeight, 1, 1000);

        camera.position.x = 0;
        camera.position.y = 0;
        camera.position.z = 45;

        scene.add(camera);

        var loader = new THREE.TextureLoader();

        loader.load('earth.jpg', function(texture) {
            material = new THREE.MeshBasicMaterial({ map: texture });
            geometry = new THREE.SphereGeometry(10, 50, 50);

            sphere = new  THREE.Mesh(geometry,material);

            scene.add(sphere);

            render();
        });
    }

    function render() {
        sphere.rotation.y += 0.01;
        
        renderer.render(scene,camera);
        requestAnimationFrame(render);
    }
    return {
        initScene: InitScene
    }
})();