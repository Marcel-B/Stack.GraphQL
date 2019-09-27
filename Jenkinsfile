node {

    def mvnHome
    def commitId
    
    stage('preparation') { 
        checkout scm
        commitId = sh(returnStdout: true, script: 'git rev-parse HEAD')
    }

    try{
        stage('restore') {
            sh 'dotnet restore'
        }
    }catch(Exception ex){
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return 
    }

    try{
        stage('build'){
            sh 'dotnet build -c Release'
        }
    }catch(Exception ex){
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }

    try{
        stage('publish'){
            sh 'dotnet publish -c Release'
        }
    }catch(Exception ex){
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }

    try{
        stage('test') {
            gitlabCommitStatus("test") {
                sh 'dotnet test' // /p:CollectCoverage=true /p:Include="[Website.Nuqneh.de]*"'
            }
        }
    }catch(Exception ex){
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }

    try{
        if(env.BRANCH_NAME == 'master'){
            stage('containerize'){
                mvnHome = env.BUILD_NUMBER
                sh "docker build -t docker.qaybe.de/stack.datalayer:0.0.${mvnHome} ."
                withDockerRegistry(credentialsId: 'DockerRegistry', toolName: 'QaybeDocker', url: 'https://docker.qaybe.de') {
                    sh "docker push docker.qaybe.de/stack.datalayer:0.0.${mvnHome}"   
                }
            }
        }
    }catch(Exception ex){
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }
    
    try{
        stage('clean'){
            cleanWs()
        }
    }catch(Exception ex){
        currentBuild.result = 'FAILURE'
        echo "RESULT: ${currentBuild.result}"
        return
    }        
}
