node {
    def mvnHome
    def commitId
    properties([gitLabConnection('GitLab')])

    stage('Preparation') { 
        checkout scm
        commitId = sh(returnStdout: true, script: 'git rev-parse HEAD')
        updateGitlabCommitStatus name: 'restore', state: 'pending', sha: commitId
        updateGitlabCommitStatus name: 'build', state: 'pending', sha: commitId
        updateGitlabCommitStatus name: 'publish', state: 'pending', sha: commitId
        updateGitlabCommitStatus name: 'test', state: 'pending', sha: commitId
        if(env.BRANCH_NAME == 'master'){
            updateGitlabCommitStatus name: 'containerize', state: 'pending', sha: commitId
        }
        updateGitlabCommitStatus name: 'clean', state: 'pending', sha: commitId
    }

    stage('Restore') {
        updateGitlabCommitStatus name: 'restore', state: 'running', sha: commitId 
        sh 'dotnet restore --configfile NuGet.config'
        updateGitlabCommitStatus name: 'restore', state: 'success', sha: commitId 
    }

    stage('Build'){
        updateGitlabCommitStatus name: 'build', state: 'running', sha: commitId 
        sh 'dotnet build'
        updateGitlabCommitStatus name: 'build', state: 'success', sha: commitId
    }

    stage('Publish'){
        updateGitlabCommitStatus name: 'publish', state: 'running', sha: commitId
        sh 'dotnet publish -c Release'
        updateGitlabCommitStatus name: 'publish', state: 'success', sha: commitId
    }

    stage('Tests') {
        gitlabCommitStatus("test") {
            sh 'dotnet test'
        }
    }

    if(env.BRANCH_NAME == 'master'){
        stage('Docker'){
            mvnHome = env.BUILD_NUMBER
            updateGitlabCommitStatus name: 'containerize', state: 'running', sha: commitId
            dir('Home.CollectorApi') {
                sh 'ls'
                sh "docker build -t docker.qaybe.de/stack.datalayer:1.0.${mvnHome} ."
            }
            withDockerRegistry(credentialsId: 'DockerRegistry', toolName: 'QaybeDocker', url: 'https://docker.qaybe.de') {
                sh "docker push docker.qaybe.de/stack.datalayer:1.0.${mvnHome}"
            }
            updateGitlabCommitStatus name: 'containerize', state: 'success', sha: commitId
        }   
    }

    stage('Clean Up'){
       updateGitlabCommitStatus name: 'clean', state: 'running', sha: commitId
       cleanWs()
       updateGitlabCommitStatus name: 'clean', state: 'success', sha: commitId
    }
}